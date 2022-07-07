namespace BoilerplateService.Infrastructures.Startup.PipelineExtensions
{
    public static class SwaggerPipelineExtension
    {
        public static void UseSwaggerExposer(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Boilerplate Service v1"));
            }
        }
    }
}