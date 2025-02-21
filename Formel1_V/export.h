#ifndef EXPORT_H_
#define EXPORT_H_

#ifdef _BUILDING_FORMEL1_
#define QX_FORMEL1_DLL_EXPORT QX_DLL_EXPORT_HELPER
#else // _BUILDING_FORMEL1_
#define QX_FORMEL1_DLL_EXPORT QX_DLL_IMPORT_HELPER
#endif // _BUILDING_FORMEL1_

#ifdef _BUILDING_FORMEL1_
#define QX_REGISTER_HPP_QX_FORMEL1 QX_REGISTER_HPP_EXPORT_DLL
#define QX_REGISTER_CPP_QX_FORMEL1 QX_REGISTER_CPP_EXPORT_DLL
#else // _BUILDING_FORMEL1_
#define QX_REGISTER_HPP_QX_FORMEL1 QX_REGISTER_HPP_IMPORT_DLL
#define QX_REGISTER_CPP_QX_FORMEL1 QX_REGISTER_CPP_IMPORT_DLL
#endif // _BUILDING_FORMEL1_

#endif // EXPORT_H_
