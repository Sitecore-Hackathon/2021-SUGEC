import React from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames';

const Comment = ({ UserName, Date, Body, clickable, onClick }) => (
  <div
    className={classnames('comment', { 'comment--buton': clickable })}
    onClick={() => clickable && onClick()}
  >
    <div className="comment__header">
      <p className="comment__user">{UserName}</p>
      <p className="comment__date">{Date}</p>
    </div>
    <p className="comment__body">{Body}</p>

    {clickable && (
      <div className="comment__location-badge">
        <span />
      </div>
    )}
  </div>
);

Comment.propTypes = {
  UserName: PropTypes.string,
  Date: PropTypes.string,
  Body: PropTypes.string,
  clickable: PropTypes.bool,
  onClick: PropTypes.func
};

export default Comment;
