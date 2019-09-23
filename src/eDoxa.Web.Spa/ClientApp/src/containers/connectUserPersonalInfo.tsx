import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadPersonalInfo, createPersonalInfo, updatePersonalInfo } from "reducers/user/personalInfo/actions";

const connectUserPersonalInfo = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, personalInfo, ...attributes }) => {
    useEffect((): void => {
      actions.loadPersonalInfo();
    });
    return <ConnectedComponent actions={actions} personalInfo={personalInfo} {...attributes} />;
  };

  const mapStateToProps = state => {
    return {
      personalInfo: state.user.personalInfo
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        loadPersonalInfo: () => dispatch(loadPersonalInfo()),
        createPersonalInfo: (data: any) => {
          const { year, month, day } = data.birthDate;
          data.birthDate = new Date(year, month, day);
          return dispatch(createPersonalInfo(data)).then(() => dispatch(loadPersonalInfo()));
        },
        updatePersonalInfo: (data: any) => dispatch(updatePersonalInfo(data)).then(() => dispatch(loadPersonalInfo()))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};

export default connectUserPersonalInfo;
