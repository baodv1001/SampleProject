import React from 'react';
import PropTypes from 'prop-types';
import Table from 'antd/lib/table';
import { useVT } from 'virtualizedtableforantd4';

const scrollConfig = {
  y: 500,
  x: true,
};

const SmartTable = ({
  debug = false,
  infinity = false,
  dataSource = [],
  scroll = scrollConfig,
  onFetch = () => null,
  lastId,
  ...props
}) => {
  const [vt] = useVT(
    () => ({
      onScroll: ({ isEnd }) => {
        if (isEnd) {
          onFetch();
        }
      },
      scroll: { y: scrollConfig.y },
      debug,
    }),
    [lastId, dataSource]
  );
  if (!infinity) {
    return <Table dataSource={dataSource} {...props} />;
  }
  return (
    <Table dataSource={dataSource} scroll={scroll} components={vt} pagination={false} {...props} />
  );
};

SmartTable.propTypes = {
  lastId: PropTypes.any,
  dataSource: PropTypes.any,
  debug: PropTypes.bool,
  infinity: PropTypes.bool,
  loadingIndicator: PropTypes.bool,
  onFetch: PropTypes.func,
  scroll: PropTypes.any,
};

export default SmartTable;
