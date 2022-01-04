import ProLayout from '@ant-design/pro-layout';
import React, { useState } from 'react';
import { NavLink, Route, Routes } from 'react-router-dom';
import AppFooter from '../../components/layout/Footer';
import RightContent from '../../components/layout/RightContent';
import styles from './main.module.less';

const MainLayout = props => {
  const [pathname, setPathname] = useState(window.location.pathname);

  const showRoutes = routes => {
    var result = null;
    if (routes.length > 0) {
      result = routes.map((route, index) => {
        return <Route key={index} path={route.path} exact={route.exact} element={route.page} />;
      });
    }
    return result;
  };

  return (
    <ProLayout
      title="Simple vote app"
      fixSiderbar
      fixedHeader
      layout="mix"
      contentWidth="Fluid"
      navTheme="dark"
      route={props.menuItems}
      location={{
        pathname,
      }}
      // onPageChange={param => {
      //   setPathname(param.pathname || '/');
      // }}
      headerTitleRender={(logo, title) => (
        <NavLink to="/" onClick={() => setPathname('/')}>
          {logo}
          {title}
        </NavLink>
      )}
      menuItemRender={(item, dom) => (
        <NavLink to={item.path} onClick={() => setPathname(item.path || '/')}>
          {dom}
        </NavLink>
      )}
      rightContentRender={() => <RightContent />}
      footerRender={() => <AppFooter />}>
      <div className={styles.container}>
        <Routes>{showRoutes(props.routes)}</Routes>
      </div>
    </ProLayout>
  );
};

export default MainLayout;
