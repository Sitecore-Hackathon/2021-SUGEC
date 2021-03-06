import { useState, useEffect, useMemo } from 'react';
import Axios from 'axios';

const useApi = url => {
  const [comments, setComments] = useState([]);
  const [loading, setLoading] = useState(false);
  const [connected, setConnected] = useState(true);

  // const connection = useMemo(() => {
  //   try {
  //     // eslint-disable-next-line no-undef
  //     return new signalR.HubConnectionBuilder()
  //       .withUrl('/sitecore/signalr/commentsHub')
  //       .build();
  //   } catch (error) {
  //     console.log(error);
  //   }
  // }, []);

  // useEffect(() => {
  //   if (connection) {
  //     connection.on('ReceiveComment', (UserName, Body, Date, Location) => {
  //       setComments([...comments, { UserName, Body, Date, Location }]);
  //     });

  //     connection
  //       .start()
  //       .then(() => {
  //         setConnected(true);
  //       })
  //       .catch(err => {
  //         return console.error(err.toString());
  //       });
  //   }
  //   // eslint-disable-next-line react-hooks/exhaustive-deps
  // }, []);

  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        const { data } = await Axios({ url });

        setComments(data);
        setLoading(false);
      } catch (error) {
        console.log(error);
        setLoading(false);
      }
    };
    fetchData();
  }, [url]);

  const postComment = async ({ UserName, Body, Location }) => {
    try {
      setLoading(true);
      const { data } = await Axios({
        method: 'post',
        url,
        data: {
          UserName,
          Body,
          Location: Location ? JSON.stringify(Location) : null
        }
      });
      // Remove this line when signalR is connected
      setComments([...comments, { ...data }]);
    } catch (error) {
      console.log(error);
    } finally {
      setLoading(false);
    }
  };

  return { comments, postComment, connected, loading };
};

export default useApi;
