import { takeLatest } from 'redux-saga/effects';
import * as employeeActions from '../actions/employees';
import { createEmployee, deleteEmployee, fetchEmployees, updateEmployee } from '../sagas/employees';
import { fetchAuthSaga, fetchUserSaga } from './auth';
import * as authActions from '../actions/auth';

export default function* mySaga() {
  //employee
  yield takeLatest(employeeActions.getEmployees.getEmployeesRequest, fetchEmployees);
  yield takeLatest(employeeActions.createEmployee.createEmployeeRequest, createEmployee);
  yield takeLatest(employeeActions.updateEmployee.updateEmployeeRequest, updateEmployee);
  yield takeLatest(employeeActions.deleteEmployee.deleteEmployeeRequest, deleteEmployee);
  //auth
  yield takeLatest(authActions.getAuth.getAuthRequest, fetchAuthSaga);
  yield takeLatest(authActions.getUser.getUserRequest, fetchUserSaga);
}
