import React from "react";
import { Component } from "react";
import { Route, Redirect } from "react-router-dom";
import {
  ApplicationPaths,
  QueryParameterNames
} from "utils/oidc/ApiAuthorizationConstants";
import authorizeService from "utils/oidc/AuthorizeService";
import { compose } from "recompose";
import { withUser } from "utils/oidc/containers";
import { Loading } from "components/Shared/Loading";

class ComponentEnhancer extends Component<any, any> {
  _subscription: any;
  constructor(props) {
    super(props);

    this.state = {
      ready: false,
      authenticated: false
    };
  }

  componentDidMount() {
    this._subscription = authorizeService.subscribe(() =>
      this.authenticationChanged()
    );
    this.populateAuthenticationState();
  }

  componentWillUnmount() {
    authorizeService.unsubscribe(this._subscription);
  }

  render() {
    const { ready, authenticated } = this.state;
    const redirectUrl = `${ApplicationPaths.Login}?${
      QueryParameterNames.ReturnUrl
    }=${encodeURI(window.location.href)}`;
    if (!ready) {
      return <Loading />;
    } else {
      const { component: Component, user, ...rest } = this.props;
      return (
        <Route
          {...rest}
          render={() =>
            authenticated &&
            parseInt((Date.now() / 1000).toString()) < user.expires_at ? (
              <Component />
            ) : (
              <Redirect to={redirectUrl} />
            )
          }
        />
      );
    }
  }

  async populateAuthenticationState() {
    const authenticated = await authorizeService.isAuthenticated();
    this.setState({ ready: true, authenticated });
  }

  async authenticationChanged() {
    this.setState({ ready: false, authenticated: false });
    await this.populateAuthenticationState();
  }
}

const enhance = compose(withUser);

export const Authorize = enhance(ComponentEnhancer);
