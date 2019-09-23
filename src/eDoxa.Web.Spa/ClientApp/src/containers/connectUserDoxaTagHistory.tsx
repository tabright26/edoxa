import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadDoxaTagHistory, changeDoxaTag } from "reducers/user/doxaTagHistory/actions";

const connectUserDoxaTagHistory = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, doxaTag, ...attributes }) => {
    useEffect((): void => {
      actions.loadDoxaTagHistory();
    });
    return <ConnectedComponent actions={actions} doxaTag={doxaTag} {...attributes} />;
  };

  const mapStateToProps = state => {
    const doxaTagHistory = state.user.doxaTagHistory.sort((left, right) => (left.timestamp < right.timestamp ? 1 : -1));
    return {
      doxaTag: doxaTagHistory[0] || null
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        loadDoxaTagHistory: () => dispatch(loadDoxaTagHistory()),
        changeDoxaTag: (data: any) => dispatch(changeDoxaTag(data)).then(() => dispatch(loadDoxaTagHistory()))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};

export default connectUserDoxaTagHistory;
