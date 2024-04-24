#ifndef RESULTS_H
#define RESULTS_H

class Drivers;

class QX_FORMEL1_DLL_EXPORT Results
{
public:
    typedef std::shared_ptr<Drivers> drivers_ptr;

    int resultId;
    drivers_ptr driver;
    int position;
    Results() { ; }
    virtual ~Results() { ; }
};

QX_REGISTER_PRIMARY_KEY(Results, int);
QX_REGISTER_HPP_QX_FORMEL1(Results, qx::trait::no_base_class_defined, 0);

typedef std::shared_ptr<Results> result_ptr;
typedef QList<result_ptr> list_results;

#endif // RESULTS_H
