using System;

namespace network
{
    [Serializable]
    public class Response
    {
        private ResponseType type;
        private object data;

        private Response() { }

        public ResponseType Type
        {
            get { return type; }
        }

        public object Data
        {
            get { return data; }
        }

        public override string ToString()
        {
            return $"Response{{type='{type}', data='{data}'}}";
        }

        public class Builder
        {
            private Response response = new Response();

            public Builder Type(ResponseType type)
            {
                response.SetType(type);
                return this;
            }

            public Builder Data(object data)
            {
                response.SetData(data);
                return this;
            }

            public Response Build()
            {
                return response;
            }
        }

        private void SetType(ResponseType type)
        {
            this.type = type;
        }

        private void SetData(object data)
        {
            this.data = data;
        }
    }
}
