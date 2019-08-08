import React, { Component } from "react";
import userManager, { POST_LOGIN_REDIRECT_URI } from "../../../utils/userManager";
import { connect } from "react-redux";
import { CallbackComponent } from "redux-oidc";
import { push } from "react-router-redux";

const loading = () => (
  <div className="animated fadeIn pt-3 text-center">
    <div className="sk-spinner sk-spinner-pulse" />
  </div>
);

class Callback extends Component {
  render() {
    return (
      <CallbackComponent
        route={POST_LOGIN_REDIRECT_URI}
        userManager={userManager}
        successCallback={() => {
          const postLoginRedirectUri = localStorage.getItem(POST_LOGIN_REDIRECT_URI);
          localStorage.removeItem(POST_LOGIN_REDIRECT_URI);
          this.props.dispatch(push(postLoginRedirectUri));
        }}
        errorCallback={error => {
          this.props.dispatch(push("/"));
          console.error(error);
        }}
      >
        {loading()}
      </CallbackComponent>
    );
  }
}

export default connect()(Callback);
