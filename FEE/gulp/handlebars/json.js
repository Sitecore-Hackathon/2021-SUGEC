module.exports = (context, options) => {
  return typeof context === 'string'
    ? options.fn(JSON.parse(context))
    : options.fn(context);
};
