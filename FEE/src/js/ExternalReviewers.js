const React = require('react');
const { render } = require('react-dom');

import { Provider } from './context';
import App from './App';

const container = document.getElementById('external-reviewers-container');

render(
  <Provider itemName={container.dataset.itemname}>
    <App src={container.dataset.src} />
  </Provider>,
  container
);
