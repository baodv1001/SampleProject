import { Avatar, Badge, Image, Popover } from 'antd';
import React from 'react';
import Content from './Content';

const RightContent = () => {
  return (
    <div>
      <Popover placement="bottomRight" content={<Content />} trigger="hover">
        <Badge dot>
          <Avatar src={<Image src="https://joeschmoe.io/api/v1/random" style={{ width: 32 }} />} />
        </Badge>
      </Popover>
    </div>
  );
};

export default RightContent;
