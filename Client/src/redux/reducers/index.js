import { combineReducers } from 'redux';
import employeesReducer from './employee.js';
import authReducer from './auth';

const appReducer = combineReducers({
  employees: employeesReducer,
  auth: authReducer,
});
const rootReducer = (state, action) => {
  if (action.type === 'USER_LOGOUT') {
    return appReducer(undefined, action);
  }

  return appReducer(state, action);
};

export default rootReducer;
