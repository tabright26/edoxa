import React, { Component } from "react";
import { connect } from "react-redux";
import { SubmissionError } from "redux-form";
import actions from "../actions/identity";
import { loadPersonalInfo, createPersonalInfo, updatePersonalInfo } from "../actions/identity/creators";

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
              case actions.CREATE_PERSONAL_INFO_SUCCESS:
                await dispatch(loadPersonalInfo());
                break;
              case actions.CREATE_PERSONAL_INFO_FAIL:
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
              case actions.UPDATE_PERSONAL_INFO_SUCCESS:
                await dispatch(loadPersonalInfo());
                break;
              case actions.UPDATE_PERSONAL_INFO_FAIL:
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
