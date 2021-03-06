module.exports = (context, options) => {
  return context
    .split(',')
    .map((chunk, index) =>
      options.fn(chunk.trim(), { data: { index, first: index === 0 } })
    )
    .join('');
};
