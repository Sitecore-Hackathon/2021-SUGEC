import React, { useContext, useState } from 'react';
import PropTypes from 'prop-types';

import { Context } from '../context';

const CommentForm = ({ onSubmit, disabled }) => {
  const { returnCheck, setReturnCheck, connected, loading } = useContext(
    Context
  );

  const [comment, setComment] = useState('');

  const handleSubmit = async e => {
    e.preventDefault();

    if (comment) {
      await onSubmit(comment);
      setComment('');
    }
  };

  const handleKeyDown = async e => {
    if (e.keyCode === 13 && returnCheck) {
      e.preventDefault();

      if (comment) {
        await onSubmit(comment);
        setComment('');
      }
    }
  };

  return (
    <form className="panel-form" onSubmit={handleSubmit}>
      <textarea
        aria-label="Add a comment"
        placeholder="Add comment..."
        onChange={e => setComment(e.target.value)}
        onKeyDown={handleKeyDown}
        value={comment}
        disabled={!connected || loading}
        required
      ></textarea>
      <div className="panel-form__footer">
        <label className="panel-form__checkbox">
          <input
            type="checkbox"
            checked={returnCheck}
            onChange={() => setReturnCheck(!returnCheck)}
          />
          <span className="checkmark">
            <svg
              height="417pt"
              viewBox="0 -46 417.81333 417"
              width="417pt"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path d="m159.988281 318.582031c-3.988281 4.011719-9.429687 6.25-15.082031 6.25s-11.09375-2.238281-15.082031-6.25l-120.449219-120.46875c-12.5-12.5-12.5-32.769531 0-45.246093l15.082031-15.085938c12.503907-12.5 32.75-12.5 45.25 0l75.199219 75.203125 203.199219-203.203125c12.503906-12.5 32.769531-12.5 45.25 0l15.082031 15.085938c12.5 12.5 12.5 32.765624 0 45.246093zm0 0" />
            </svg>
          </span>
          Press enter to send
        </label>
        {!returnCheck && (
          <button type="submit" disabled={!connected || loading}>
            Send
          </button>
        )}
      </div>
    </form>
  );
};

CommentForm.propTypes = {
  onSubmit: PropTypes.func,
  disabled: PropTypes.bool
};

export default CommentForm;
