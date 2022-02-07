import { HomeOutlined, UserOutlined} from '@ant-design/icons';
import Employee from 'page/Employee';
import EditEmployee from 'page/Employee/EmployeeDetail';
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
    path: '/employee/edit/:id',
    exact: true,
    page: <EditEmployee />,
  },
  {
    path: '/employee/add',
    exact: true,
    page: <EditEmployee />,
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
