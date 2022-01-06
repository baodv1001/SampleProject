import NotAuthorized from 'page/NotAuthorized';
import Login from '../page/Login';
import Register from '../page/Register';

const guestRoutes = [
  {
    path: '/login',
    exact: true,
    page: <Login />,
  },
  {
    path: '/register',
    exact: true,
    page: <Register />,
  },
  {
    path: '*',
    page: <NotAuthorized />,
  },
];
export { guestRoutes };
