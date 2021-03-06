import React from 'react';
import PropTypes from 'prop-types';

const Comment = ({ UserName, Date, Body }) => (
  <div className="comment">
    <div className="comment__header">
      <p className="comment__user">{UserName}</p>
      <p className="comment__date">{Date}</p>
    </div>
    <p className="comment__body">{Body}</p>
  </div>
);

Comment.propTypes = {
  UserName: PropTypes.string,
  Date: PropTypes.string,
  Body: PropTypes.string
};

export default Comment;
