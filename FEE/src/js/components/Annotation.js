import React, { useState } from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames';

import Comment from './Comment';

const Annotation = ({ Location, UserName, Date, Body }) => {
  const [isOpen, setIsOpen] = useState(false);

  return (
    <div
      className={classnames('annotation-modal')}
      style={Location}
      onClick={e => e.stopPropagation()}
    >
      <button
        className="annotation-modal__pin"
        onClick={() => setIsOpen(!isOpen)}
      />
      {isOpen && <Comment {...{ UserName, Date, Body }} />}
    </div>
  );
};

Annotation.propTypes = {
  Location: PropTypes.object.isRequired,
  UserName: PropTypes.string,
  Date: PropTypes.string,
  Body: PropTypes.string
};

export default Annotation;
