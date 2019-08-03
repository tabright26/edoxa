import React, { Component } from 'react';
import userManager, { POST_LOGIN_REDIRECT_URI } from './userManager';
import { connect } from 'react-redux';
import { CallbackComponent } from 'redux-oidc';
import { push } from 'react-router-redux';

class Callback extends Component {
  render() {
    return (
      <CallbackComponent
        userManager={userManager}
        successCallback={() => {
          const postLoginRedirectUri = localStorage.getItem(
            POST_LOGIN_REDIRECT_URI
          );
          localStorage.removeItem(POST_LOGIN_REDIRECT_URI);
          this.props.dispatch(push(postLoginRedirectUri));
        }}
        errorCallback={error => {
          this.props.dispatch(push('/'));
          console.error(error);
        }}
      >
        <div>Redirecting...</div>
      </CallbackComponent>
    );
  }
}

export default connect()(Callback);
