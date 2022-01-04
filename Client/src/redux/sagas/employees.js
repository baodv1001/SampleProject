import { call, put } from 'redux-saga/effects';
import employeeApi from '../../api/employeeApi';
import * as employeeActions from '../actions/employees';

export function* fetchEmployees(action) {
  try {
    const employees = yield call(employeeApi.getAll);

    yield put(employeeActions.getEmployees.getEmployeesSuccess(employees));
  } catch (error) {
    yield put(employeeActions.getEmployees.getEmployeesFailure(error));
  }
}

export function* createEmployee(action) {
  try {
    const newEmployee = yield call(employeeApi.create, action.payload);

    yield put(employeeActions.createEmployee.createEmployeeSuccess(newEmployee));
  } catch (error) {
    yield put(employeeActions.createEmployee.createEmployeeFailure(error));
  }
}

export function* updateEmployee(action) {
  try {
    yield call(employeeApi.update, action.payload);
    yield put(employeeActions.updateEmployee.updateEmployeeSuccess(action.payload));
  } catch (error) {
    yield put(employeeActions.updateEmployee.updateEmployeeFailure(error));
  }
}

export function* deleteEmployee(action) {
  try {
    yield call(employeeApi.delete, action.payload);

    yield put(employeeActions.deleteEmployee.deleteEmployeeSuccess(action.payload));
  } catch (error) {
    yield put(employeeActions.deleteEmployee.deleteEmployeeFailure(error));
  }
}
