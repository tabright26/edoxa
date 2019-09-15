import React, { Component } from "react";
import { connect } from "react-redux";
import { SubmissionError } from "redux-form";
import { loadPersonalInfo, createPersonalInfo, updatePersonalInfo } from "actions/identity/creators";
import actionTypes from "actions/identity";

const withPersonalInfo = WrappedComponent => {
  class PersonalInfoContainer extends Component {
    componentDidMount() {
      this.props.actions.loadPersonalInfo();
    }

    render() {
      return <WrappedComponent {...this.props} />;
    }
  }

  const mapStateToProps = state => {
    return {
      personalInfo: state.user.personalInfo
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        loadPersonalInfo: () => dispatch(loadPersonalInfo()),
        createPersonalInfo: async data => {
          const { year, month, day } = data.birthDate;
          data.birthDate = new Date(year, month, day);
          await dispatch(createPersonalInfo(data)).then(async action => {
            switch (action.type) {
              case actionTypes.CREATE_PERSONAL_INFO_SUCCESS:
                await dispatch(loadPersonalInfo());
                break;
              case actionTypes.CREATE_PERSONAL_INFO_FAIL:
                const { isAxiosError, response } = action.error;
                if (isAxiosError) {
                  throw new SubmissionError(response.data.errors);
                }
                break;
              default:
                console.error(action);
                break;
            }
          });
        },
        updatePersonalInfo: async data => {
          await dispatch(updatePersonalInfo(data)).then(async action => {
            switch (action.type) {
              case actionTypes.UPDATE_PERSONAL_INFO_SUCCESS:
                await dispatch(loadPersonalInfo());
                break;
              case actionTypes.UPDATE_PERSONAL_INFO_FAIL:
                const { isAxiosError, response } = action.error;
                if (isAxiosError) {
                  throw new SubmissionError(response.data.errors);
                }
                break;
              default:
                console.error(action);
                break;
            }
          });
        }
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(PersonalInfoContainer);
};

export default withPersonalInfo;
