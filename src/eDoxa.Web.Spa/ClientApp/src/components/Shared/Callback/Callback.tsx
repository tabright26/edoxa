import React, { FunctionComponent } from "react";
import userManager, { POST_LOGIN_REDIRECT_URI } from "store/middlewares/oidc/userManager";
import { connect } from "react-redux";
import { CallbackComponent } from "redux-oidc";
import { push } from "react-router-redux";
import Loading from "components/Shared/Override/Loading";

const Callback: FunctionComponent<any> = ({ dispatch }) => {
  return (
    <CallbackComponent
      userManager={userManager}
      successCallback={() => {
        dispatch(push(localStorage.getItem(POST_LOGIN_REDIRECT_URI) || "/"));
        localStorage.removeItem(POST_LOGIN_REDIRECT_URI);
      }}
      errorCallback={error => {
        console.error(error);
        dispatch(push("/"));
      }}
    >
      <Loading />
    </CallbackComponent>
  );
};

export default connect()(Callback);
