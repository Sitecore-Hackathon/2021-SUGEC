import React from 'react';
import PropTypes from 'prop-types';
import dayjs from 'dayjs';

const CommentsPanel = ({ UserName, Date, Body }) => (
  <div className="comment">
    <div className="comment__header">
      <p className="comment__user">{UserName}</p>
      <p className="comment__date">
        {dayjs(Date).format('dddd, MMMM D, YYYY')}
      </p>
    </div>
    <p className="comment__body">{Body}</p>
  </div>
);

CommentsPanel.propTypes = {
  UserName: PropTypes.string,
  Date: PropTypes.string,
  Body: PropTypes.string
};

export default CommentsPanel;
