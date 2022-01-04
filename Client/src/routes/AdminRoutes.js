import { HomeOutlined, UserOutlined} from '@ant-design/icons';
import Employee from 'page/Employee';
import Home from '../page/Home';
import NotFound from '../page/NotFound';
const routes = [
  {
    path: '/',
    exact: true,
    page: <Home />,
  },
  {
    path: '/employee',
    exact: true,
    page: <Employee />,
  },
  {
    path: '*',
    page: <NotFound />,
  },
];
const menuItems = {
  path: '/',
  routes: [
    {
      path: '/',
      name: 'Home',
      component: <Home />,
      icon: <HomeOutlined />,
    },
    {
      path: '/employee',
      name: 'Employee',
      component: <Employee />,
      icon: <UserOutlined />,
    },
  ],
};
export { routes, menuItems };
