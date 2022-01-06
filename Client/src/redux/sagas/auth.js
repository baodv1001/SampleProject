import { call, put } from 'redux-saga/effects';
import authApi from '../../api/authApi';
import * as authActions from '../actions/auth';

export function* fetchAuthSaga(action) {
  try {
    const auth = yield call(authApi.login, action.payload);

    yield put(authActions.getAuth.getAuthSuccess(auth));
  } catch (error) {
    yield put(authActions.getAuth.getAuthFailure(error));
  }
}
export function* fetchUserSaga(action) {
  try {
    const user = yield call(authApi.getUser, action.payload);

    yield put(authActions.getUser.getUserSuccess(user));
  } catch (error) {
    yield put(authActions.getUser.getUserFailure(error));
  }
}
