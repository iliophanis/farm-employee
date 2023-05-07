const useDisableConsoleApi = () => {
  const handleDisableConsoleApi = () => {
    if (process.env.NODE_ENV === 'production') {
      const actions = [
        'assert',
        'clear',
        'count',
        'debug',
        'dir',
        'dirxml',
        'error',
        'exception',
        'group',
        'groupCollapsed',
        'groupEnd',
        'info',
        'log',
        'markTimeline',
        'profile',
        'profileEnd',
        'table',
        'time',
        'timeEnd',
        'timeline',
        'timelineEnd',
        'timeStamp',
        'trace',
        'warn',
      ];
      actions.forEach((method: string) => {
        //@ts-ignore
        window.console[method] = () => {};
      });
    }
  };
  return { handleDisableConsoleApi };
};

export default useDisableConsoleApi;
