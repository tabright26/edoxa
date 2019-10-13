import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadPersonalInfo, createPersonalInfo, updatePersonalInfo } from "store/root/user/personalInfo/actions";
import { RootState } from "store/root/types";

export const connectUserPersonalInfo = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, personalInfo, ...attributes }) => {
    useEffect((): void => {
      actions.loadPersonalInfo();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <ConnectedComponent actions={actions} personalInfo={personalInfo} {...attributes} />;
  };

  const mapStateToProps = (state: RootState) => {
    return {
      personalInfo: state.user.personalInfo
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
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
