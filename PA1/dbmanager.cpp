#include "dbmanager.h"
#include <QSqlQuery>
#include <iostream>

dbmanager::dbmanager()
{
    db = QSqlDatabase::addDatabase("QSQLITE");
    db.setDatabaseName("../PA1.db");
    db.open();

    std::cout << db.isOpen() << std::endl;
}

dbmanager::~dbmanager()
{
    db.close();
}

bool dbmanager::addNewEntry(QString path, QString filename, int priority, QString desc)
{
    if (checkIfPriorityExists(priority))
        return false;

    QSqlQuery query;
    query.prepare("insert into Images (path, filename, priority, description) values (:path, :filename, :priority, :description)");
    query.bindValue(":path", path);
    query.bindValue(":filename", filename);
    query.bindValue(":priority", priority);
    query.bindValue(":description", desc);
    query.exec();

    return true;
}

pic dbmanager::getNextEntry(int currenPrio)
{
    QSqlQuery query;
    query.exec("select * from Images where priority = (select min(priority) from Images where priority > " + QString::number(currenPrio) + ")");

    pic pic;
    if (query.next()) {
        pic.path = query.value(1).toString();
        pic.name = query.value(2).toString();
        pic.priority = query.value(3).toInt();
        pic.description = query.value(4).toString();
    } else {
        query = db.exec("select * from Images where priority = (select min(priority) from images)");
        query.next();
        pic.path = query.value(1).toString();
        pic.name = query.value(2).toString();
        pic.priority = query.value(3).toInt();
        pic.description = query.value(4).toString();
    }

    return pic;
}

bool dbmanager::checkIfPriorityExists(int priority)
{
    QSqlQuery query;
    query = db.exec("select * from Images where priority = " + QString::number(priority));

    while (query.next()) {
        return true;
    }

    return false;
}
