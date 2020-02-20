import ReactGA from "react-ga";
import { history } from "utils/router/history";

export function initializeReactGA(): void {
  if (process.env.NODE_ENV === "production") {
    ReactGA.initialize(process.env.REACT_APP_GA_TRACKING_CODE);
    history.listen(location => ReactGA.pageview(location.pathname));
  }
}

export function publishUserCreatedEvent() {
  if (process.env.NODE_ENV === "production") {
    ReactGA.event({
      category: "Account",
      action: "User registered to platform",
      label: "User Created"
    });
  }
}
