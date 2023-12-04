#ifndef DBMANAGER_H
#define DBMANAGER_H

#include <QSqlDatabase>
#include "models.h"

class dbmanager
{
public:
    dbmanager();
    ~dbmanager();

    bool addNewEntry(QString path, QString filename, int priority, QString desc);
    pic getNextEntry(int currenPrio);

private:
    QSqlDatabase db;
    bool checkIfPriorityExists(int priority);
};

#endif // DBMANAGER_H
