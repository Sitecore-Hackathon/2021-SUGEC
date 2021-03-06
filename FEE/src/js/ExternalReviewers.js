const React = require('react');
const { render } = require('react-dom');
import CommentsPanel from './modules/CommentsPanel';

document.addEventListener('DOMContentLoaded', () => {
  const panelDiv = document.createElement('div');
  document.querySelector('body').appendChild(panelDiv);
  render(<CommentsPanel />, panelDiv);
});
