import React, { useContext } from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames';
import CommentForm from './CommentForm';

import { Context } from '../context';

const AnnotationModal = ({ Location, isOpen, onClose }) => {
  const { userName, postComment } = useContext(Context);

  const handlePostComent = async Body => {
    await postComment({ UserName: userName, Body, Location });
    onClose();
  };

  return (
    <div
      className={classnames('annotation-modal', {
        'annotation-modal--hidden': !isOpen
      })}
      style={Location}
      onClick={e => e.stopPropagation()}
    >
      <div className="annotation-modal__pin" />
      <div className="annotation-modal__inner">
        <div className="annotation-modal__close">
          <button type="button" onClick={onClose}>
            <svg
              height="329pt"
              viewBox="0 0 329.26933 329"
              width="329pt"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path d="m194.800781 164.769531 128.210938-128.214843c8.34375-8.339844 8.34375-21.824219 0-30.164063-8.339844-8.339844-21.824219-8.339844-30.164063 0l-128.214844 128.214844-128.210937-128.214844c-8.34375-8.339844-21.824219-8.339844-30.164063 0-8.34375 8.339844-8.34375 21.824219 0 30.164063l128.210938 128.214843-128.210938 128.214844c-8.34375 8.339844-8.34375 21.824219 0 30.164063 4.15625 4.160156 9.621094 6.25 15.082032 6.25 5.460937 0 10.921875-2.089844 15.082031-6.25l128.210937-128.214844 128.214844 128.214844c4.160156 4.160156 9.621094 6.25 15.082032 6.25 5.460937 0 10.921874-2.089844 15.082031-6.25 8.34375-8.339844 8.34375-21.824219 0-30.164063zm0 0" />
            </svg>
          </button>
        </div>
        <CommentForm onSubmit={handlePostComent} />
      </div>
    </div>
  );
};

AnnotationModal.propTypes = {
  Location: PropTypes.object.isRequired,
  isOpen: PropTypes.bool.isRequired,
  onClose: PropTypes.func.isRequired
};

export default AnnotationModal;
