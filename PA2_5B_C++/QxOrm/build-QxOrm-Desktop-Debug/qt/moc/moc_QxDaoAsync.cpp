/****************************************************************************
** Meta object code from reading C++ file 'QxDaoAsync.h'
**
** Created by: The Qt Meta Object Compiler version 68 (Qt 6.4.2)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include <memory>
#include "../../../include/QxDao/QxDaoAsync.h"
#include <QtCore/qmetatype.h>
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'QxDaoAsync.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 68
#error "This file was generated using the moc from 6.4.2. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

#ifndef Q_CONSTINIT
#define Q_CONSTINIT
#endif

QT_BEGIN_MOC_NAMESPACE
QT_WARNING_PUSH
QT_WARNING_DISABLE_DEPRECATED
namespace {
struct qt_meta_stringdata_qx__dao__detail__QxDaoAsyncRunner_t {
    uint offsetsAndSizes[16];
    char stringdata0[34];
    char stringdata1[14];
    char stringdata2[1];
    char stringdata3[10];
    char stringdata4[9];
    char stringdata5[38];
    char stringdata6[11];
    char stringdata7[15];
};
#define QT_MOC_LITERAL(ofs, len) \
    uint(sizeof(qt_meta_stringdata_qx__dao__detail__QxDaoAsyncRunner_t::offsetsAndSizes) + ofs), len 
Q_CONSTINIT static const qt_meta_stringdata_qx__dao__detail__QxDaoAsyncRunner_t qt_meta_stringdata_qx__dao__detail__QxDaoAsyncRunner = {
    {
        QT_MOC_LITERAL(0, 33),  // "qx::dao::detail::QxDaoAsyncRu..."
        QT_MOC_LITERAL(34, 13),  // "queryFinished"
        QT_MOC_LITERAL(48, 0),  // ""
        QT_MOC_LITERAL(49, 9),  // "QSqlError"
        QT_MOC_LITERAL(59, 8),  // "daoError"
        QT_MOC_LITERAL(68, 37),  // "qx::dao::detail::QxDaoAsyncPa..."
        QT_MOC_LITERAL(106, 10),  // "pDaoParams"
        QT_MOC_LITERAL(117, 14)   // "onQueryStarted"
    },
    "qx::dao::detail::QxDaoAsyncRunner",
    "queryFinished",
    "",
    "QSqlError",
    "daoError",
    "qx::dao::detail::QxDaoAsyncParams_ptr",
    "pDaoParams",
    "onQueryStarted"
};
#undef QT_MOC_LITERAL
} // unnamed namespace

Q_CONSTINIT static const uint qt_meta_data_qx__dao__detail__QxDaoAsyncRunner[] = {

 // content:
      10,       // revision
       0,       // classname
       0,    0, // classinfo
       2,   14, // methods
       0,    0, // properties
       0,    0, // enums/sets
       0,    0, // constructors
       0,       // flags
       1,       // signalCount

 // signals: name, argc, parameters, tag, flags, initial metatype offsets
       1,    2,   26,    2, 0x06,    1 /* Public */,

 // slots: name, argc, parameters, tag, flags, initial metatype offsets
       7,    1,   31,    2, 0x0a,    4 /* Public */,

 // signals: parameters
    QMetaType::Void, 0x80000000 | 3, 0x80000000 | 5,    4,    6,

 // slots: parameters
    QMetaType::Void, 0x80000000 | 5,    6,

       0        // eod
};

Q_CONSTINIT const QMetaObject qx::dao::detail::QxDaoAsyncRunner::staticMetaObject = { {
    QMetaObject::SuperData::link<QObject::staticMetaObject>(),
    qt_meta_stringdata_qx__dao__detail__QxDaoAsyncRunner.offsetsAndSizes,
    qt_meta_data_qx__dao__detail__QxDaoAsyncRunner,
    qt_static_metacall,
    nullptr,
    qt_incomplete_metaTypeArray<qt_meta_stringdata_qx__dao__detail__QxDaoAsyncRunner_t,
        // Q_OBJECT / Q_GADGET
        QtPrivate::TypeAndForceComplete<QxDaoAsyncRunner, std::true_type>,
        // method 'queryFinished'
        QtPrivate::TypeAndForceComplete<void, std::false_type>,
        QtPrivate::TypeAndForceComplete<const QSqlError &, std::false_type>,
        QtPrivate::TypeAndForceComplete<qx::dao::detail::QxDaoAsyncParams_ptr, std::false_type>,
        // method 'onQueryStarted'
        QtPrivate::TypeAndForceComplete<void, std::false_type>,
        QtPrivate::TypeAndForceComplete<qx::dao::detail::QxDaoAsyncParams_ptr, std::false_type>
    >,
    nullptr
} };

void qx::dao::detail::QxDaoAsyncRunner::qt_static_metacall(QObject *_o, QMetaObject::Call _c, int _id, void **_a)
{
    if (_c == QMetaObject::InvokeMetaMethod) {
        auto *_t = static_cast<QxDaoAsyncRunner *>(_o);
        (void)_t;
        switch (_id) {
        case 0: _t->queryFinished((*reinterpret_cast< std::add_pointer_t<QSqlError>>(_a[1])),(*reinterpret_cast< std::add_pointer_t<qx::dao::detail::QxDaoAsyncParams_ptr>>(_a[2]))); break;
        case 1: _t->onQueryStarted((*reinterpret_cast< std::add_pointer_t<qx::dao::detail::QxDaoAsyncParams_ptr>>(_a[1]))); break;
        default: ;
        }
    } else if (_c == QMetaObject::IndexOfMethod) {
        int *result = reinterpret_cast<int *>(_a[0]);
        {
            using _t = void (QxDaoAsyncRunner::*)(const QSqlError & , qx::dao::detail::QxDaoAsyncParams_ptr );
            if (_t _q_method = &QxDaoAsyncRunner::queryFinished; *reinterpret_cast<_t *>(_a[1]) == _q_method) {
                *result = 0;
                return;
            }
        }
    }
}

const QMetaObject *qx::dao::detail::QxDaoAsyncRunner::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->dynamicMetaObject() : &staticMetaObject;
}

void *qx::dao::detail::QxDaoAsyncRunner::qt_metacast(const char *_clname)
{
    if (!_clname) return nullptr;
    if (!strcmp(_clname, qt_meta_stringdata_qx__dao__detail__QxDaoAsyncRunner.stringdata0))
        return static_cast<void*>(this);
    return QObject::qt_metacast(_clname);
}

int qx::dao::detail::QxDaoAsyncRunner::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
{
    _id = QObject::qt_metacall(_c, _id, _a);
    if (_id < 0)
        return _id;
    if (_c == QMetaObject::InvokeMetaMethod) {
        if (_id < 2)
            qt_static_metacall(this, _c, _id, _a);
        _id -= 2;
    } else if (_c == QMetaObject::RegisterMethodArgumentMetaType) {
        if (_id < 2)
            *reinterpret_cast<QMetaType *>(_a[0]) = QMetaType();
        _id -= 2;
    }
    return _id;
}

// SIGNAL 0
void qx::dao::detail::QxDaoAsyncRunner::queryFinished(const QSqlError & _t1, qx::dao::detail::QxDaoAsyncParams_ptr _t2)
{
    void *_a[] = { nullptr, const_cast<void*>(reinterpret_cast<const void*>(std::addressof(_t1))), const_cast<void*>(reinterpret_cast<const void*>(std::addressof(_t2))) };
    QMetaObject::activate(this, &staticMetaObject, 0, _a);
}
namespace {
struct qt_meta_stringdata_qx__QxDaoAsync_t {
    uint offsetsAndSizes[18];
    char stringdata0[15];
    char stringdata1[13];
    char stringdata2[1];
    char stringdata3[38];
    char stringdata4[11];
    char stringdata5[14];
    char stringdata6[10];
    char stringdata7[9];
    char stringdata8[16];
};
#define QT_MOC_LITERAL(ofs, len) \
    uint(sizeof(qt_meta_stringdata_qx__QxDaoAsync_t::offsetsAndSizes) + ofs), len 
Q_CONSTINIT static const qt_meta_stringdata_qx__QxDaoAsync_t qt_meta_stringdata_qx__QxDaoAsync = {
    {
        QT_MOC_LITERAL(0, 14),  // "qx::QxDaoAsync"
        QT_MOC_LITERAL(15, 12),  // "queryStarted"
        QT_MOC_LITERAL(28, 0),  // ""
        QT_MOC_LITERAL(29, 37),  // "qx::dao::detail::QxDaoAsyncPa..."
        QT_MOC_LITERAL(67, 10),  // "pDaoParams"
        QT_MOC_LITERAL(78, 13),  // "queryFinished"
        QT_MOC_LITERAL(92, 9),  // "QSqlError"
        QT_MOC_LITERAL(102, 8),  // "daoError"
        QT_MOC_LITERAL(111, 15)   // "onQueryFinished"
    },
    "qx::QxDaoAsync",
    "queryStarted",
    "",
    "qx::dao::detail::QxDaoAsyncParams_ptr",
    "pDaoParams",
    "queryFinished",
    "QSqlError",
    "daoError",
    "onQueryFinished"
};
#undef QT_MOC_LITERAL
} // unnamed namespace

Q_CONSTINIT static const uint qt_meta_data_qx__QxDaoAsync[] = {

 // content:
      10,       // revision
       0,       // classname
       0,    0, // classinfo
       3,   14, // methods
       0,    0, // properties
       0,    0, // enums/sets
       0,    0, // constructors
       0,       // flags
       2,       // signalCount

 // signals: name, argc, parameters, tag, flags, initial metatype offsets
       1,    1,   32,    2, 0x06,    1 /* Public */,
       5,    2,   35,    2, 0x06,    3 /* Public */,

 // slots: name, argc, parameters, tag, flags, initial metatype offsets
       8,    2,   40,    2, 0x08,    6 /* Private */,

 // signals: parameters
    QMetaType::Void, 0x80000000 | 3,    4,
    QMetaType::Void, 0x80000000 | 6, 0x80000000 | 3,    7,    4,

 // slots: parameters
    QMetaType::Void, 0x80000000 | 6, 0x80000000 | 3,    7,    4,

       0        // eod
};

Q_CONSTINIT const QMetaObject qx::QxDaoAsync::staticMetaObject = { {
    QMetaObject::SuperData::link<QThread::staticMetaObject>(),
    qt_meta_stringdata_qx__QxDaoAsync.offsetsAndSizes,
    qt_meta_data_qx__QxDaoAsync,
    qt_static_metacall,
    nullptr,
    qt_incomplete_metaTypeArray<qt_meta_stringdata_qx__QxDaoAsync_t,
        // Q_OBJECT / Q_GADGET
        QtPrivate::TypeAndForceComplete<QxDaoAsync, std::true_type>,
        // method 'queryStarted'
        QtPrivate::TypeAndForceComplete<void, std::false_type>,
        QtPrivate::TypeAndForceComplete<qx::dao::detail::QxDaoAsyncParams_ptr, std::false_type>,
        // method 'queryFinished'
        QtPrivate::TypeAndForceComplete<void, std::false_type>,
        QtPrivate::TypeAndForceComplete<const QSqlError &, std::false_type>,
        QtPrivate::TypeAndForceComplete<qx::dao::detail::QxDaoAsyncParams_ptr, std::false_type>,
        // method 'onQueryFinished'
        QtPrivate::TypeAndForceComplete<void, std::false_type>,
        QtPrivate::TypeAndForceComplete<const QSqlError &, std::false_type>,
        QtPrivate::TypeAndForceComplete<qx::dao::detail::QxDaoAsyncParams_ptr, std::false_type>
    >,
    nullptr
} };

void qx::QxDaoAsync::qt_static_metacall(QObject *_o, QMetaObject::Call _c, int _id, void **_a)
{
    if (_c == QMetaObject::InvokeMetaMethod) {
        auto *_t = static_cast<QxDaoAsync *>(_o);
        (void)_t;
        switch (_id) {
        case 0: _t->queryStarted((*reinterpret_cast< std::add_pointer_t<qx::dao::detail::QxDaoAsyncParams_ptr>>(_a[1]))); break;
        case 1: _t->queryFinished((*reinterpret_cast< std::add_pointer_t<QSqlError>>(_a[1])),(*reinterpret_cast< std::add_pointer_t<qx::dao::detail::QxDaoAsyncParams_ptr>>(_a[2]))); break;
        case 2: _t->onQueryFinished((*reinterpret_cast< std::add_pointer_t<QSqlError>>(_a[1])),(*reinterpret_cast< std::add_pointer_t<qx::dao::detail::QxDaoAsyncParams_ptr>>(_a[2]))); break;
        default: ;
        }
    } else if (_c == QMetaObject::IndexOfMethod) {
        int *result = reinterpret_cast<int *>(_a[0]);
        {
            using _t = void (QxDaoAsync::*)(qx::dao::detail::QxDaoAsyncParams_ptr );
            if (_t _q_method = &QxDaoAsync::queryStarted; *reinterpret_cast<_t *>(_a[1]) == _q_method) {
                *result = 0;
                return;
            }
        }
        {
            using _t = void (QxDaoAsync::*)(const QSqlError & , qx::dao::detail::QxDaoAsyncParams_ptr );
            if (_t _q_method = &QxDaoAsync::queryFinished; *reinterpret_cast<_t *>(_a[1]) == _q_method) {
                *result = 1;
                return;
            }
        }
    }
}

const QMetaObject *qx::QxDaoAsync::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->dynamicMetaObject() : &staticMetaObject;
}

void *qx::QxDaoAsync::qt_metacast(const char *_clname)
{
    if (!_clname) return nullptr;
    if (!strcmp(_clname, qt_meta_stringdata_qx__QxDaoAsync.stringdata0))
        return static_cast<void*>(this);
    return QThread::qt_metacast(_clname);
}

int qx::QxDaoAsync::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
{
    _id = QThread::qt_metacall(_c, _id, _a);
    if (_id < 0)
        return _id;
    if (_c == QMetaObject::InvokeMetaMethod) {
        if (_id < 3)
            qt_static_metacall(this, _c, _id, _a);
        _id -= 3;
    } else if (_c == QMetaObject::RegisterMethodArgumentMetaType) {
        if (_id < 3)
            *reinterpret_cast<QMetaType *>(_a[0]) = QMetaType();
        _id -= 3;
    }
    return _id;
}

// SIGNAL 0
void qx::QxDaoAsync::queryStarted(qx::dao::detail::QxDaoAsyncParams_ptr _t1)
{
    void *_a[] = { nullptr, const_cast<void*>(reinterpret_cast<const void*>(std::addressof(_t1))) };
    QMetaObject::activate(this, &staticMetaObject, 0, _a);
}

// SIGNAL 1
void qx::QxDaoAsync::queryFinished(const QSqlError & _t1, qx::dao::detail::QxDaoAsyncParams_ptr _t2)
{
    void *_a[] = { nullptr, const_cast<void*>(reinterpret_cast<const void*>(std::addressof(_t1))), const_cast<void*>(reinterpret_cast<const void*>(std::addressof(_t2))) };
    QMetaObject::activate(this, &staticMetaObject, 1, _a);
}
QT_WARNING_POP
QT_END_MOC_NAMESPACE
