import React, { useState } from 'react';
import PropTypes from 'prop-types';

const LoginForm = ({ onSubmit }) => {
  const [username, setUsername] = useState('');

  const handleSubmit = e => {
    e.preventDefault();

    if (username) {
      onSubmit(username);
      setUsername('');
    }
  };

  return (
    <form className="panel-form panel-form--inline" onSubmit={handleSubmit}>
      <input
        aria-label="Username"
        type="text"
        value={username}
        onChange={e => setUsername(e.target.value)}
        placeholder="Username"
        required
      />
      <button type="submit">Login</button>
    </form>
  );
};

LoginForm.propTypes = {
  onSubmit: PropTypes.func
};

export default LoginForm;
