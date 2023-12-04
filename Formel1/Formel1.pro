include(../QxOrm/QxOrm.pri)

QT       += core gui
QT       += charts

greaterThan(QT_MAJOR_VERSION, 6): QT += widgets

CONFIG += c++17

DEFINES += _BUILDING_FONE_

INCLUDEPATH += ../../QxOrm/include/
LIBS += -L"../../QxOrm/lib/" -lQxOrmd
PRECOMPILED_HEADER = "precompiled.h"

# You can make your code fail to compile if it uses deprecated APIs.
# In order to do so, uncomment the following line.
#DEFINES += QT_DISABLE_DEPRECATED_BEFORE=0x060000    # disables all the APIs deprecated before Qt 6.0.0

SOURCES += \
    constructors.cpp \
    drivers.cpp \
    main.cpp \
    mainwindow.cpp \
    races.cpp \
    results.cpp

HEADERS += \
    constructors.h \
    drivers.h \
    export.h \
    mainwindow.h \
    precompiled.h \
    races.h \
    results.h

FORMS += \
    mainwindow.ui

# Default rules for deployment.
qnx: target.path = /tmp/$${TARGET}/bin
else: unix:!android: target.path = /opt/$${TARGET}/bin
!isEmpty(target.path): INSTALLS += target
