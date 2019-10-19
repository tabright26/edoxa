import { connectRouter } from "connected-react-router";

import { history } from "utils/router/config";

export const reducer = connectRouter(history);
