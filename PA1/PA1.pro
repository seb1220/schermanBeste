QT       += core gui
QT += sql

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

CONFIG += c++17

# You can make your code fail to compile if it uses deprecated APIs.
# In order to do so, uncomment the following line.
#DEFINES += QT_DISABLE_DEPRECATED_BEFORE=0x060000    # disables all the APIs deprecated before Qt 6.0.0

SOURCES += \
    dbmanager.cpp \
    main.cpp \
    mainwindow.cpp \
    picturewidget.cpp

HEADERS += \
    dbmanager.h \
    mainwindow.h \
    models.h \
    picturewidget.h

FORMS += \
    mainwindow.ui \
    picturewidget.ui

# Default rules for deployment.
qnx: target.path = /tmp/$${TARGET}/bin
else: unix:!android: target.path = /opt/$${TARGET}/bin
!isEmpty(target.path): INSTALLS += target

win32:CONFIG(release, debug|release): LIBS += -L$$PWD/TinyExif/build-TinyExif-Desktop-Debug/release/ -lTinyExif
else:win32:CONFIG(debug, debug|release): LIBS += -L$$PWD/TinyExif/build-TinyExif-Desktop-Debug/debug/ -lTinyExif
else:unix: LIBS += -L$$PWD/TinyExif/build-TinyExif-Desktop-Debug/ -lTinyExif

INCLUDEPATH += $$PWD/TinyExif/build-TinyExif-Desktop-Debug
DEPENDPATH += $$PWD/TinyExif/build-TinyExif-Desktop-Debug
