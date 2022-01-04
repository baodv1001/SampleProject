import { Button } from 'antd';
import React from 'react';
import { useNavigate } from 'react-router-dom';

const Content = props => {
  const navigate = useNavigate();

  const logout = () => {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('idUser');
    navigate('/login');
    window.location.reload();
  };
  return (
    <div>
      <Button style={{ marginRight: '8px' }} onClick={logout}>
        Log out
      </Button>
    </div>
  );
};

export default Content;
