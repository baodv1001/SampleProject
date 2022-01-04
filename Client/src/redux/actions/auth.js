import { createActions } from 'redux-actions';
export const getType = reduxAction => {
  return reduxAction().type;
};

export const getAuth = createActions({
  getAuthRequest: payload => payload,
  getAuthSuccess: payload => payload,
  getAuthFailure: err => err,
});

export const getUser = createActions({
  getUserRequest: payload => payload,
  getUserSuccess: payload => payload,
  getUserFailure: err => err,
});
