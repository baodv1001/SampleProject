import INIT_STATE from '../constant';
import { getType } from '../actions/auth';
import * as authActions from '../actions/auth';
export default function authReducer(state = INIT_STATE.auth, action) {
  switch (action.type) {
    // get Auth
    case getType(authActions.getAuth.getAuthRequest):
      return {
        ...state,
        data: action.payload,
        isLoading: true,
      };
    case getType(authActions.getAuth.getAuthSuccess):
      return {
        ...state,
        data: action.payload,
        isLoading: false,
      };
    case getType(authActions.getAuth.getAuthFailure):
      return {
        ...state,
        isLoading: false,
      };
    case getType(authActions.getUser.getUserRequest):
      return {
        ...state,
        data: action.payload,
        isLoading: true,
      };
    case getType(authActions.getUser.getUserSuccess):
      return {
        ...state,
        data: action.payload,
        isLoading: false,
      };
    case getType(authActions.getUser.getUserFailure):
      return {
        ...state,
        isLoading: false,
      };
    default:
      return state;
  }
}
