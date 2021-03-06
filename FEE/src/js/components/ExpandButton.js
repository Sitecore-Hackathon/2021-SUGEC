import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames';

const ExpandButton = ({ onClick, hidden }) => (
  <button
    className={classnames('expand-button', { 'expand-button--hidden': hidden })}
    onClick={onClick}
  >
    <svg
      xmlns="http://www.w3.org/2000/svg"
      x="0px"
      y="0px"
      viewBox="0 0 60.016 60.016"
    >
      <path d="M42.008,0h-24c-9.925,0-18,8.075-18,18v14c0,9.59,7.538,17.452,17,17.973v8.344c0,0.937,0.764,1.699,1.703,1.699 c0.449,0,0.874-0.178,1.195-0.499l1.876-1.876C26.708,52.714,33.259,50,40.227,50h1.781c9.925,0,18-8.075,18-18V18 C60.008,8.075,51.933,0,42.008,0z M17.008,29c-2.206,0-4-1.794-4-4s1.794-4,4-4s4,1.794,4,4S19.213,29,17.008,29z M30.008,29 c-2.206,0-4-1.794-4-4s1.794-4,4-4s4,1.794,4,4S32.213,29,30.008,29z M43.008,29c-2.206,0-4-1.794-4-4s1.794-4,4-4s4,1.794,4,4 S45.213,29,43.008,29z" />
    </svg>
  </button>
);

ExpandButton.propTypes = {
  onClick: PropTypes.func,
  hidden: PropTypes.bool
};

export default ExpandButton;
