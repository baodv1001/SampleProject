import { Button, Result } from 'antd';
import React from 'react';
import { useNavigate } from 'react-router';
const NotAuthorized = () => {
  const navigate = useNavigate();

  return (
    <div>
      <Result
        status="403"
        title="403"
        subTitle="Sorry, you are not authorized to access this page."
        extra={
          <Button
            type="primary"
            onClick={() => {
              navigate('/login');
            }}>
            Back to Login
          </Button>
        }
      />
      ,
    </div>
  );
};

export default NotAuthorized;
