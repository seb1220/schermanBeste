
#include <queue>
#include <iostream>
#include "flightplaner.h"

flightplaner::flightplaner(QWidget *parent)
    : QMainWindow(parent)
{
    ui.setupUi(this);

    connect(ui.cb_from, SIGNAL(currentIndexChanged(int)), this, SLOT(findFlightPaths()));
    connect(ui.cb_to, SIGNAL(currentIndexChanged(int)), this, SLOT(findFlightPaths()));
    connect(ui.cb_airline, SIGNAL(currentIndexChanged(int)), this, SLOT(findFlightPaths()));

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
    if (ui.cb_from->currentText() == nullptr && ui.cb_to->currentText() == nullptr) {
        return;
    }

    std::cout << "Finding flight paths..." << std::endl;

    airport from = db.airports[ui.cb_from->currentText()];
    airport to = db.airports[ui.cb_to->currentText()];
    airline airline;
    for (const auto& a : db.airlines.values()) {
        if (a.name == ui.cb_airline->currentText()) {
            airline = a;
            break;
        }
    }

    ui.widget->clearCities();
    ui.widget->addCity(QPointF(from.longitude, to.latitude));

    QMap<int, std::pair<int, bool>> visited; // TODO: initialize with infinity
    for (const auto& airport : db.airports.values()) {
        visited[airport.id] = {INT_MAX, false};
    }
    std::queue<std::pair<int, airport>> paths;

    std::cout << "Finding flight paths...2" << std::endl;

    paths.emplace(0, from);
    while (!paths.empty()) {
        airport current_airport = paths.front().second;
        int hops = paths.front().first;
        paths.pop();

        if (current_airport.id == to.id) {
//            ui.widget->addCity(QPointF(current_airport.longitude, current_airport.latitude));
            std::cout << "Found path with " << hops << " hops." << std::endl;
            break;
        }

        for (const auto& flight : db.flights) {
            int weight = 1002;
            if (flight.airline_id == airline.id)
                weight = 1000;
            else if (db.airlines[flight.airline_id].alliance_id == airline.alliance_id) {
                weight = 1001;
            }

            if (flight.departure_airport_id != current_airport.id || visited[flight.arrival_airport_id].second ||
                visited[flight.arrival_airport_id].first < hops + weight)
                continue;

            visited[flight.arrival_airport_id].first = current_airport.id;
            paths.emplace(hops + weight, db.airports[db.airport_indexes[flight.arrival_airport_id]]);
        }

        visited[current_airport.id].second = true;
    }
}
