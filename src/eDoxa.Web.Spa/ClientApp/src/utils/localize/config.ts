import { InitializePayload } from "react-localize-redux";
import { renderToStaticMarkup } from "react-dom/server";

export const config: InitializePayload = {
  languages: [{ name: "English", code: "en" }],
  options: {
    defaultLanguage: "en",
    renderToStaticMarkup
  }
};
