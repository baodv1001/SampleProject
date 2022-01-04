import INIT_STATE from '../constant';
import { getType } from '../actions/employees';
import * as employeeActions from '../actions/employees';

export default function employeesReducer(state = INIT_STATE.employees, action) {
  switch (action.type) {
    //get
    case getType(employeeActions.getEmployees.getEmployeesRequest):
      return {
        ...state,
        isLoading: true,
        isSuccess: false,
      };
    case getType(employeeActions.getEmployees.getEmployeesSuccess):
      return {
        ...state,
        data: action.payload,
        isLoading: false,
        isSuccess: true,
      };
    case getType(employeeActions.getEmployees.getEmployeesFailure):
      return {
        ...state,
        isLoading: false,
        isSuccess: false,
      };
    // create
    case getType(employeeActions.createEmployee.createEmployeeRequest):
      return {
        ...state,
        isLoading: true,
        isSuccess: false,
      };
    case getType(employeeActions.createEmployee.createEmployeeSuccess):
      return {
        ...state,
        data: [...state.data, action.payload],
        isLoading: false,
        isSuccess: true,
      };
    case getType(employeeActions.createEmployee.createEmployeeFailure):
      return {
        ...state,
        isLoading: false,
        isSuccess: false,
      };
    //update
    case getType(employeeActions.updateEmployee.updateEmployeeRequest):
      return {
        ...state,
        isLoading: true,
        isSuccess: false,
      };
    case getType(employeeActions.updateEmployee.updateEmployeeSuccess):
      return {
        ...state,
        data: state.data.map(employee =>
          employee.id === action.payload.id ? action.payload : employee
        ),
        isLoading: false,
        isSuccess: true,
      };
    case getType(employeeActions.updateEmployee.updateEmployeeFailure):
      return {
        ...state,
        isLoading: false,
        isSuccess: false,
      };
    // delete
    case getType(employeeActions.deleteEmployee.deleteEmployeeRequest):
      return {
        ...state,
        isLoading: true,
        isSuccess: false,
      };
    case getType(employeeActions.deleteEmployee.deleteEmployeeSuccess):
      return {
        ...state,
        data: state.data.filter(employee => employee.idEmployee !== action.payload),
        isLoading: false,
        isSuccess: true,
      };
    case getType(employeeActions.deleteEmployee.deleteEmployeeFailure):
      return {
        ...state,
        isLoading: false,
        isSuccess: false,
      };
    default:
      return state;
  }
}
