
#include <queue>
#include <iostream>
#include "flightplaner.h"

flightplaner::flightplaner(QWidget *parent)
    : QMainWindow(parent)
{
    ui.setupUi(this);

    connect(ui.startSearch, SIGNAL(clicked()), this, SLOT(findFlightPaths()));

    for (const auto& airport : db.airports) {
        ui.cb_from->addItem(airport.name);
        ui.cb_to->addItem(airport.name);
    }

    for (const auto& airline : db.airlines) {
        ui.cb_airline->addItem(airline.name);
    }
}

flightplaner::~flightplaner()
{}

void flightplaner::findFlightPaths() {
    if (ui.cb_from->currentText() == nullptr || ui.cb_to->currentText() == nullptr || ui.cb_from->currentText() == ui.cb_to->currentText()) {
        return;
    }

    std::cout << "Finding flight paths..." << std::endl;

    airport from = db.airports[ui.cb_from->currentText()];
    airport to = db.airports[ui.cb_to->currentText()];
    airline airline;
    for (const auto& a : db.airlines.values())
        if (a.name == ui.cb_airline->currentText()) {
            airline = a;
            break;
        }

    ui.widget->clear();
    ui.widget->addCity(QPointF(from.longitude, from.latitude));
    ui.widget->addCity(QPointF(to.longitude, to.latitude));

    QMap<int, std::pair<std::pair<int, int>, bool>> visited;
    for (int id: db.airport_indexes.keys())
        visited[id] = {{INT_MAX, -1}, false};

    auto compareByFirstElement = [](const auto& pair1, const auto& pair2) {
        return pair1.first > pair2.first;
    };
    std::priority_queue<std::pair<int, airport>, std::vector<std::pair<int, airport>>, decltype(compareByFirstElement)> paths(compareByFirstElement);

    paths.emplace(0, from);

    while (!paths.empty()) {
        airport current_airport = paths.top().second;
        int hops = paths.top().first;
        paths.pop();

        if (visited[current_airport.id].second)
            continue;

        if (current_airport.id == to.id) {
            std::cout << "Found path with " << hops << " hops." << std::endl;
            break;
        }

        for (const auto& flight : db.flights) {
            int weight = 1002;
            if (flight.airline_id == airline.id)
                weight = 1000;
            else if (airline.alliance_id != 0 && db.airlines[flight.airline_id].alliance_id == airline.alliance_id) {
                weight = 1001;
            }

            if (flight.departure_airport_id != current_airport.id ||
                visited[flight.arrival_airport_id].second ||
                visited[flight.arrival_airport_id].first.first <= hops + weight)
                continue;

            auto added_airport = db.airports[db.airport_indexes[flight.arrival_airport_id]];

            visited[flight.arrival_airport_id].first.first = hops + weight;
            visited[flight.arrival_airport_id].first.second = current_airport.id;
            paths.emplace(hops + weight, added_airport);
        }

        visited[current_airport.id].second = true;
    }

    if (visited[to.id].first.second != -1) {
        std::cout << "Path found." << std::endl;
        int current_id = to.id;
        while (current_id != -1) {
            ui.widget->addCity(QPointF(db.airports[db.airport_indexes[current_id]].longitude,
                                       db.airports[db.airport_indexes[current_id]].latitude));
            current_id = visited[current_id].first.second;
        }
    } else {
        std::cout << "No path found." << std::endl;
    }
}
