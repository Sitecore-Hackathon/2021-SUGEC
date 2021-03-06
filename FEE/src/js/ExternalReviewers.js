const React = require('react');
const { render } = require('react-dom');

import { Provider } from './context';
import App from './App';

const container = document.getElementById('external-reviewers-container');

render(
  <Provider>
    <App src={container.dataset.src} />
  </Provider>,
  container
);
