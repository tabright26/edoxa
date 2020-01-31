import ReactGA from "react-ga";
import { history } from "utils/router/history";
import { REACT_APP_GA_TRACKING_CODE } from "keys";

export function initializeReactGA(): void {
  if (process.env.NODE_ENV === "production") {
    ReactGA.initialize(REACT_APP_GA_TRACKING_CODE);
    history.listen(location => ReactGA.pageview(location.pathname));
  }
}
