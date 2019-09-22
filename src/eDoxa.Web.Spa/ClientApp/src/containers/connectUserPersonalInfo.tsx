import React, { Component } from "react";
import { connect } from "react-redux";
import { SubmissionError } from "redux-form";
import { IAxiosAction } from "interfaces/axios";
import { loadPersonalInfo, createPersonalInfo, updatePersonalInfo } from "actions/identity/actionCreators";
import { CreatePersonalInfoActionType, UpdatePersonalInfoActionType } from "actions/identity/actionTypes";

const connectUserPersonalInfo = WrappedComponent => {
  class Container extends Component<any> {
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
          await dispatch(createPersonalInfo(data)).then(async (action: IAxiosAction<CreatePersonalInfoActionType>) => {
            switch (action.type) {
              case "CREATE_PERSONAL_INFO_SUCCESS":
                await dispatch(loadPersonalInfo());
                break;
              case "CREATE_PERSONAL_INFO_FAIL":
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
          await dispatch(updatePersonalInfo(data)).then(async (action: IAxiosAction<UpdatePersonalInfoActionType>) => {
            switch (action.type) {
              case "UPDATE_PERSONAL_INFO_SUCCESS":
                await dispatch(loadPersonalInfo());
                break;
              case "UPDATE_PERSONAL_INFO_FAIL":
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
  )(Container);
};

export default connectUserPersonalInfo;
