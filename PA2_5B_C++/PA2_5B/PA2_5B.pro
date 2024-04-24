include(../QxOrm/QxOrm.pri)
QT       += core gui charts

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

CONFIG += c++17

# You can make your code fail to compile if it uses deprecated APIs.
# In order to do so, uncomment the following line.
#DEFINES += QT_DISABLE_DEPRECATED_BEFORE=0x060000    # disables all the APIs deprecated before Qt 6.0.0

INCLUDEPATH += $$PWD/../QxOrm/include
DEFINES += _BUILDING_PA2
PRECOMPILED_HEADER = precompiled.h

win32:CONFIG(release, debug|release): LIBS += -L$$PWD/../QxOrm/lib/ -lQxOrm
else:win32:CONFIG(debug, debug|release): LIBS += -L$$PWD/../QxOrm/lib/ -lQxOrmd
else:unix: LIBS += -L$$PWD/../QxOrm/lib/ -lQxOrmd

SOURCES += \
    main.cpp \
    mainwindow.cpp \
    quakes.cpp

HEADERS += \
    export.h \
    mainwindow.h \
    precompiled.h \
    quakes.h

FORMS += \
    mainwindow.ui

# Default rules for deployment.
qnx: target.path = /tmp/$${TARGET}/bin
else: unix:!android: target.path = /opt/$${TARGET}/bin
!isEmpty(target.path): INSTALLS += target
