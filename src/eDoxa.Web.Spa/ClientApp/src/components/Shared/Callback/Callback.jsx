import React, { Component } from "react";
import userManager, { POST_LOGIN_REDIRECT_URI } from "../../../utils/userManager";
import { connect } from "react-redux";
import { CallbackComponent } from "redux-oidc";
import { push } from "react-router-redux";
import Loading from "../Loading";

class Callback extends Component {
  render() {
    return (
      <CallbackComponent
        userManager={userManager}
        successCallback={() => {
          this.props.dispatch(push(localStorage.getItem(POST_LOGIN_REDIRECT_URI) || "/"));
          localStorage.removeItem(POST_LOGIN_REDIRECT_URI);
        }}
        errorCallback={error => {
          console.error(error);
          this.props.dispatch(push("/"));
        }}
      >
        <Loading.Default />
      </CallbackComponent>
    );
  }
}

export default connect()(Callback);
