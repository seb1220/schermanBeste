include (../QxOrm/QxOrm.pri)

QT       += core gui charts

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

CONFIG += c++17

DEFINES += _BUILDING_FORMEL1_

INCLUDEPATH += ../QxOrm/include/
LIBS += -L"$$PWD/../QxOrm/lib/" -lQxOrmd
PRECOMPILED_HEADER = "precompiled.h"

# You can make your code fail to compile if it uses deprecated APIs.
# In order to do so, uncomment the following line.
#DEFINES += QT_DISABLE_DEPRECATED_BEFORE=0x060000    # disables all the APIs deprecated before Qt 6.0.0

SOURCES += \
    constructors.cpp \
    drivers.cpp \
    main.cpp \
    mainwindow.cpp \
    results.cpp

HEADERS += \
    constructors.h \
    drivers.h \
    export.h \
    mainwindow.h \
    precompiled.h \
    results.h

FORMS += \
    mainwindow.ui

# Default rules for deployment.
qnx: target.path = /tmp/$${TARGET}/bin
else: unix:!android: target.path = /opt/$${TARGET}/bin
!isEmpty(target.path): INSTALLS += target
