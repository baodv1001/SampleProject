import { ConfigProvider } from 'antd';
import enUS from 'antd/lib/locale/en_US';
import { useEffect, useState } from 'react';
import { BrowserRouter } from 'react-router-dom';
import { menuItems, routes } from '../routes/AdminRoutes';
import { guestRoutes } from '../routes/GuestRoutes';
import Admin from './Admin';
import './App.less';
import Guest from './Guest';

function App() {
  const [route, setRoute] = useState(<Guest routes={guestRoutes}></Guest>);
  useEffect(() => {
    if (localStorage.getItem('accessToken')) {
      setRoute(<Admin routes={routes} menuItems={menuItems}></Admin>);
    }
  }, []);

  return (
    <ConfigProvider locale={enUS}>
      <BrowserRouter>{route}</BrowserRouter>
    </ConfigProvider>
  );
}

export default App;
