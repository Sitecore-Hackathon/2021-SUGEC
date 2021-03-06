import React, { useState } from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames';

import Comment from './Comment';

const Annotation = ({ Location, UserName, Date, Body, isOpen, onClick }) => {
  return (
    <div
      className={classnames('annotation-modal', {
        'annotation-modal--right': Location.left > 1000
      })}
      style={Location}
      onClick={e => e.stopPropagation()}
    >
      <button className="annotation-modal__pin" onClick={onClick} />
      {isOpen && <Comment {...{ UserName, Date, Body }} />}
    </div>
  );
};

Annotation.propTypes = {
  Location: PropTypes.object.isRequired,
  UserName: PropTypes.string.isRequired,
  Date: PropTypes.string.isRequired,
  Body: PropTypes.string.isRequired,
  isOpen: PropTypes.bool.isRequired,
  onClick: PropTypes.func.isRequired
};

export default Annotation;
